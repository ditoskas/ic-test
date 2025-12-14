import { Pagination } from "react-bootstrap";
import type {JSX} from "react";

interface BlockHashPaginatorProps {
    totalPages: number;
    currentPage: number;
}


export default function BlockHashPaginator({ totalPages, currentPage }: BlockHashPaginatorProps) {
    const items: JSX.Element[] = [];
    for (let number = 1; number <= totalPages; number++) {
        items.push(
            <Pagination.Item key={number} active={number === currentPage}>
                {number}
            </Pagination.Item>,
        );
    }
    const hasItems = items.length > 0;
    if (!hasItems) {
        return null;
    }
    return (
        <div className="w-100">
            <Pagination>{items}</Pagination>
        </div>
    );
}