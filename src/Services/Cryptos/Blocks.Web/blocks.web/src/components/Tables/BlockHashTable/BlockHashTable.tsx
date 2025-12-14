import type {BlockTransaction, PagedResponse} from "../../../types";
import BlockHashPaginator from "./BlockHashPaginator.tsx";
import BlockHashPageSizer from "./BlockHashPageSizer.tsx";


interface BlockHashTableProps {
    pageInfo:  PagedResponse<BlockTransaction> | null;
}

export default function BlockHashTable({ pageInfo }: BlockHashTableProps) {
    if (!pageInfo || pageInfo.payload.data.length == 0) {
        return <div className={"text-center w-100"}>No blocks to display.</div>;
    }

    return (
        <>
            <BlockHashPageSizer />
            <div style={{ maxHeight: "500px", overflowY: "auto" }}>
            <table className="table table-striped table-hover">
                <thead>
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Chain</th>
                    <th scope="col">Hash</th>
                    <th scope="col">Received Time</th>
                    <th scope="col">Created At</th>
                </tr>
                </thead>
                <tbody>
                {pageInfo.payload.data.map(block => (
                    <tr key={block.id}>
                        <td>{block.id}</td>
                        <td>{block.chain}</td>
                        <td>{block.hash}</td>
                        <td>{block.receivedTime}</td>
                        <td>{block.createdAt}</td>
                    </tr>
                ))}
                </tbody>
            </table>
            </div>
            <BlockHashPaginator totalPages={pageInfo.payload?.totalPages} />
        </>
    );
}
