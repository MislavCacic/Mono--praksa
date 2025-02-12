import PeopleManager from "./components/PeopleManager";

function App() {
	return (
		<div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-blue-200 to-blue-400 p-6">
			<div className="bg-white shadow-2xl rounded-lg p-6 w-full max-w-md">
				<h1 className="text-3xl font-bold text-center text-blue-600 mb-4">
					Unos Osoba
				</h1>
				<PeopleManager />
			</div>
		</div>
	);
}

export default App;
