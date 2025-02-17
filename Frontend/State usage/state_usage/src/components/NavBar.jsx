import { Link } from "react-router-dom";

function NavBar() {
	return (
		<nav style={{ padding: "10px", background: "#ddd" }}>
			<Link to="/" style={{ marginRight: "10px" }}>
				Početna
			</Link>
			<Link to="/add-user" style={{ marginRight: "10px" }}>
				Dodaj korisnika
			</Link>
			<Link to="/users">Lista korisnika</Link>
		</nav>
	);
}

export default NavBar;
