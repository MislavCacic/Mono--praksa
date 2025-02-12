import AddButton from "./Components/AddButton";
import DeleteButton from "./Components/DeleteButton";
import EditButton from "./Components/EditButton";
import Grid from "./Components/Grid";
import PopUp from "./Components/PopUp";
import UpdateButton from "./Components/UpdateButton";

const items = ["Item 1", "Item 2", "Item 3", "Item 4"];

function App() {
	return (
		<div className="flex flex-col items-center space-y-4 p-4">
			<AddButton />
			<DeleteButton />
			<UpdateButton />
			<EditButton />
			<Grid data={items} />
			<PopUp />
		</div>
	);
}

export default App;
