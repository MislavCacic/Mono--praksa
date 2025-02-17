import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { addPerson, editPerson } from "../service/UserService";

function UserForm({ setReloadUsers, people, setPeople }) {
	const [person, setPerson] = useState({ first_name: "", last_name: "" });
	const [successMessage, setSuccessMessage] = useState("");
	const navigate = useNavigate();
	const location = useLocation();
	const params = new URLSearchParams(location.search);
	const editId = params.get("id");

	useEffect(() => {
		if (editId) {
			const userToEdit = people.find(
				(person) => person.id === parseInt(editId)
			);
			if (userToEdit) {
				setPerson({
					first_name: userToEdit.first_name,
					last_name: userToEdit.last_name,
				});
			}
		}
	}, [editId, people]);

	function handleChange(e) {
		setPerson({ ...person, [e.target.name]: e.target.value });
	}

	function handleSubmit(e) {
		e.preventDefault();
		if (!person.first_name || !person.last_name) return;

		if (editId) {
			editPerson(parseInt(editId), person, setPeople, people);
		} else {
			addPerson(person, setPeople, people, setSuccessMessage);
		}

		setReloadUsers(true);
		setTimeout(() => {
			setSuccessMessage("");
			navigate("/users");
		}, 1500);
	}

	return (
		<div>
			<h1>{editId ? "Uredi korisnika" : "Dodaj korisnika"}</h1>

			{successMessage && <p style={{ color: "green" }}>{successMessage}</p>}

			<form onSubmit={handleSubmit}>
				<div>
					<label>Ime:</label>
					<input
						type="text"
						name="first_name"
						value={person.first_name}
						onChange={handleChange}
						required
					/>
				</div>
				<div>
					<label>Prezime:</label>
					<input
						type="text"
						name="last_name"
						value={person.last_name}
						onChange={handleChange}
						required
					/>
				</div>
				<button type="submit">
					{editId ? "Spremi promjene" : "Dodaj osobu"}
				</button>
			</form>
		</div>
	);
}

export default UserForm;
