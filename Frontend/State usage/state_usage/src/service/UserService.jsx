import axios from "axios";

const API_URL = "https://reqres.in/api/users";

export async function fetchUsers(setPeople) {
	const response = await axios.get(API_URL);
	setPeople(response.data.data);
}

export async function addPerson(person, setPeople, people) {
	await axios.post(API_URL, person);
	const newPerson = {
		id: people.length + 1,
		first_name: person.first_name,
		last_name: person.last_name,
	};
	setPeople([...people, newPerson]);
}

export async function editPerson(id, updatedData, setPeople, people) {
	await axios.put(`${API_URL}/${id}`, updatedData);
	const updatedPeople = people.map((person) =>
		person.id === id ? { ...person, ...updatedData } : person
	);
	setPeople(updatedPeople);
}

export async function deletePerson(id, setPeople, people) {
	setPeople(people.filter((person) => person.id !== id));
	await axios.delete(`${API_URL}/${id}`);
}

export async function searchById(id, setFoundedPerson) {
	const response = await axios.get(`${API_URL}/${id}`);
	setFoundedPerson(response.data.data);
}
