export default function EditButton() {
	const openPopUp = () => {
		window.open("/popup?action=Edit", "_blank", "width=400,height=300");
	};

	return (
		<button
			className="px-4 py-2 bg-green-500 text-white rounded-lg shadow-md"
			onClick={openPopUp}
		>
			Edit
		</button>
	);
}
