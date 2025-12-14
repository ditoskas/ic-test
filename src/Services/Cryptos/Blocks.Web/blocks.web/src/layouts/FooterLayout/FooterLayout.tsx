export default function FooterLayout({ numberOfRecords }: { numberOfRecords: number }) {
    return (
        <footer className="bg-body-tertiary mt-auto">
            <div className="p-1 container">
                Total Number of Records: {numberOfRecords}
            </div>
        </footer>
    );
}