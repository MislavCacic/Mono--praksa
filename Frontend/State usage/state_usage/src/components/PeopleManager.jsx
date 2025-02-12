import axios from "axios";
import { useEffect, useState } from "react";
import Form from "./Form";
import PersonList from "./PersonList";

const API_URL = "https://reqres.in/api/users";

function PeopleManager() {
	const [person, setPerson] = useState({ first_name: "", last_name: "" });
	const [people, setPeople] = useState([]);
	const [editId, setEditId] = useState(null);
	const [searchId, setSearchId] = useState("");
	const [foundedPerson, setFoundedPerson] = useState(null);

	// 🚀 Dohvati korisnike čim se stranica učita
	useEffect(() => {
		axios.get(API_URL).then((response) => {
			setPeople(response.data.data);
		});
	}, []); // 📌 Pražan array [] znači da se useEffect pokreće samo JEDNOM, kad se komponenta prikaže

	function handleChange(e) {
		setPerson({ ...person, [e.target.name]: e.target.value });
	}

	function addPerson(e) {
		e.preventDefault();

		if (!person.first_name || !person.last_name) return;

		if (editId) {
			axios.put(`${API_URL}/${editId}`, person).then((response) => {
				setPeople(people.map((p) => (p.id === editId ? response.data : p)));
				setEditId(null);
			});
		} else {
			axios.post(API_URL, person).then((response) => {
				setPeople([...people, response.data]);
			});
		}

		setPerson({ first_name: "", last_name: "" });
	}

	function deletePerson(id) {
		axios.delete(`${API_URL}/${id}`).then(() => {
			setPeople(people.filter((p) => p.id !== id));
		});
	}

	function editPerson(id) {
		const user = people.find((p) => p.id === id);
		setPerson({ first_name: user.first_name, last_name: user.last_name });
		setEditId(id);
	}

	function searchById() {
		if (!searchId) {
			setFoundedPerson(null);
			return;
		}

		axios
			.get(`${API_URL}/${searchId}`)
			.then((response) => {
				setFoundedPerson(response.data.data);
			})
			.catch(() => {
				setFoundedPerson(null);
			});
	}

	return (
		<div>
			<h2>Pretraga korisnika po ID-u</h2>
			<input
				type="number"
				placeholder="Unesi ID"
				value={searchId}
				onChange={(e) => setSearchId(e.target.value)}
			/>
			<button onClick={searchById}>Pretraži</button>

			{foundedPerson && (
				<div>
					<h3>Korisnik pronađen:</h3>
					<p>ID: {foundedPerson.id}</p>
					<p>Ime: {foundedPerson.first_name}</p>
					<p>Prezime: {foundedPerson.last_name}</p>
				</div>
			)}

			<Form
				person={person}
				handleChange={handleChange}
				addPerson={addPerson}
				editId={editId}
			/>

			<PersonList
				people={people}
				deletePerson={deletePerson}
				editPerson={editPerson}
			/>
		</div>
	);
}

export default PeopleManager;
