export default function UpdateButton() {
	const openPopUp = () => {
		window.open("/popup?action=Update", "_blank", "width=400,height=300");
	};

	return (
		<button
			className="px-4 py-2 bg-yellow-500 text-white rounded-lg shadow-md"
			onClick={openPopUp}
		>
			Update
		</button>
	);
}
