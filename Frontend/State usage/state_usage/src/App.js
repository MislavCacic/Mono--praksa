import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useState } from "react";
import NavBar from "./components/NavBar";
import Home from "./components/Home";
import UserForm from "./components/UserForm";
import User from "./components/User";

function App() {
	const [reloadUsers, setReloadUsers] = useState(false);
	const [people, setPeople] = useState([]); // 🚀 Globalni state za sve korisnike

	return (
		<Router>
			<NavBar />
			<Routes>
				<Route path="/" element={<Home />} />
				<Route path="/add-user" element={<UserForm setReloadUsers={setReloadUsers} people={people} setPeople={setPeople} />} />
				<Route path="/users" element={<User reloadUsers={reloadUsers} setReloadUsers={setReloadUsers} people={people} setPeople={setPeople} />} />
			</Routes>
		</Router>
	);
}

export default App;
