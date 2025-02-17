import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { deletePerson, fetchUsers, searchById } from "../service/UserService";
import UserList from "./UserList";

function User({ reloadUsers, setReloadUsers, people, setPeople }) {
	const [searchId, setSearchId] = useState("");
	const [foundedPerson, setFoundedPerson] = useState(null);
	const navigate = useNavigate(); 

	useEffect(() => {
		fetchUsers(setPeople);
		setReloadUsers(false);
	}, [reloadUsers]);

	function handleSearchById() {
		searchById(searchId, setFoundedPerson);
	}

	function handleEditUser(id) {
		navigate(`/add-user?id=${id}`);
	}

	return (
		<div>
			<h1>Lista korisnika</h1>

			<h2>Pretraga korisnika po ID-u</h2>
			<input
				type="number"
				placeholder="Unesi ID"
				value={searchId}
				onChange={(e) => setSearchId(e.target.value)}
			/>
			<button onClick={handleSearchById}>Pretraži</button>

			{foundedPerson && (
				<div>
					<h3>Korisnik pronađen:</h3>
					<p>ID: {foundedPerson.id}</p>
					<p>Ime: {foundedPerson.first_name}</p>
					<p>Prezime: {foundedPerson.last_name}</p>
				</div>
			)}

			<UserList
				people={people}
				deletePerson={(id) => deletePerson(id, setPeople, people)}
				editPerson={handleEditUser}
			/>
		</div>
	);
}

export default User;
