function UserList({ people, deletePerson, editPerson }) {
	return (
		<div>
			<h2>Popis korisnika:</h2>
			<ul>
				{people.map((person) => (
					<li key={person.id}>
						{person.first_name} {person.last_name}
						<button onClick={() => editPerson(person.id)}>Edit</button>{" "}
						<button onClick={() => deletePerson(person.id)}>Delete</button>
					</li>
				))}
			</ul>
		</div>
	);
}

export default UserList;
