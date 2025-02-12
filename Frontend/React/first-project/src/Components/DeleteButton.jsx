export default function DeleteButton() {
	const openPopUp = () => {
		window.open("/popup?action=Delete", "_blank", "width=400,height=300");
	};

	return (
		<button
			className="px-4 py-2 bg-red-500 text-white rounded-lg shadow-md"
			onClick={openPopUp}
		>
			Delete
		</button>
	);
}
