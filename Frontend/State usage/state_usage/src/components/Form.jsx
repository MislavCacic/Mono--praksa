function Form({ person, handleChange, addPerson, editId }) {
	return (
		<form onSubmit={addPerson}>
			<table>
				<tbody>
					<tr>
						<td>Ime:</td>
						<td>
							<input
								type="text"
								name="first_name"
								value={person.first_name}
								onInput={handleChange}
								placeholder="Unesi ime"
							/>
						</td>
					</tr>
					<tr>
						<td>Prezime:</td>
						<td>
							<input
								type="text"
								name="last_name"
								value={person.last_name}
								onInput={handleChange}
								placeholder="Unesi prezime"
							/>
						</td>
					</tr>
				</tbody>
			</table>
			<button type="submit">
				{editId ? "Spremi promjene" : "Dodaj osobu"}
			</button>
		</form>
	);
}

export default Form;
