import { Pagination } from "react-bootstrap";
import React, {type JSX} from "react";
import {useBlocks} from "../../../contexts/BlocksContext/BlocksContext.tsx";

interface BlockHashPaginatorProps {
    totalPages: number;
}


export default function BlockHashPaginator({ totalPages }: BlockHashPaginatorProps) {
    const { selectedPage, setSelectedPage } = useBlocks();

    function onPageItemClickHandler(e: React.MouseEvent<HTMLAnchorElement, MouseEvent>) {
        e.preventDefault();
        const pageNumber = Number(e.currentTarget.text);
        setSelectedPage(pageNumber);
    }
    const items: JSX.Element[] = [];
    for (let number = 1; number <= totalPages; number++) {
        items.push(
            <Pagination.Item key={number} active={number === selectedPage} onClick={onPageItemClickHandler}>
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