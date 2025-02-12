export default function Grid({ data }) {
	return (
		<div className="grid grid-cols-3 gap-4 p-4 w-full max-w-4xl">
			{data.length > 0 ? (
				data.map((item, index) => (
					<div key={index} className="border p-4 rounded-lg shadow-md">
						{item}
					</div>
				))
			) : (
				<p className="text-center col-span-3">No data available</p>
			)}
		</div>
	);
}
