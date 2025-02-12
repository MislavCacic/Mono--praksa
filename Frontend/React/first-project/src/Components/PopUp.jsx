export default function PopUp() {
	if (window.opener == null) {
		return null;
	}

	const queryParams = new URLSearchParams(window.location.search);
	const action = queryParams.get("action") || "Action";

	return (
		<div className="flex flex-col items-center justify-center h-screen space-y-4">
			<h2 className="text-xl font-bold">{action} Item</h2>
			<form className="flex flex-col space-y-2">
				<input
					type="text"
					placeholder="Ime"
					className="border p-2 rounded-lg"
				/>
				<input
					type="text"
					placeholder="Prezime"
					className="border p-2 rounded-lg"
				/>
				<button
					type="button"
					className="px-4 py-2 bg-blue-500 text-white rounded-lg shadow-md"
				>
					{action}
				</button>
			</form>
		</div>
	);
}
