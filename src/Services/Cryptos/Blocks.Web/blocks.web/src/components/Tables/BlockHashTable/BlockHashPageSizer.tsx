import { Form, InputGroup } from "react-bootstrap";
import React from "react";

import {useBlocks} from "../../../contexts/BlocksContext/BlocksContext.tsx";


export default function BlockHashPageSizer() {
    const availablePageSizes = [10, 25, 50, 100];
    const { selectedPageSize, setSelectedPageSize } = useBlocks();

    function handleChange(e: React.ChangeEvent<HTMLSelectElement>) {
        const pageSize = Number(e.target.value);
        setSelectedPageSize(pageSize);
    }
    return (
        <Form style={{ maxWidth: "200px" }}>
            <InputGroup>
                <InputGroup.Text id="basic-addon1">Select Size</InputGroup.Text>
                <select className="form-select" aria-label="Select blockchain" onChange={handleChange} defaultValue={selectedPageSize}>
                    {availablePageSizes.map(size => (
                        <option key={size} value={size}>
                            {size}
                        </option>
                    ))}
                </select>
            </InputGroup>
        </Form>
    );
}