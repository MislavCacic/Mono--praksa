export default function AddButton() {
	const openPopUp = () => {
		window.open("/popup?action=Add", "_blank", "width=400,height=300");
	};

	return (
		<button
			className="px-4 py-2 bg-blue-500 text-white rounded-lg shadow-md"
			onClick={openPopUp}
		>
			Add
		</button>
	);
}  