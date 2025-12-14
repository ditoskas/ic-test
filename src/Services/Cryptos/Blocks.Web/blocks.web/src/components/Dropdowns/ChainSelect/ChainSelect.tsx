import { Form, InputGroup } from "react-bootstrap";
import React, { useEffect } from "react";

import {useBlocks} from "../../../contexts/BlocksContext/BlocksContext.tsx";


export default function ChainSelect() {
    const { blockChains, setSelectedChainId, selectedChainId} = useBlocks();
    useEffect(() => {
        if (blockChains.length > 0 && selectedChainId === null) {
            setSelectedChainId(blockChains[0].id);
        }
    }, [blockChains, selectedChainId, setSelectedChainId]);

    function handleChange(e: React.ChangeEvent<HTMLSelectElement>) {
        const id = Number(e.target.value);
        const selected = blockChains.find(c => c.id === id) ?? null;
        if (!selected) {
            return;
        }
        setSelectedChainId(id);
    }
    return (
        <Form>
            <InputGroup>
                <InputGroup.Text id="basic-addon1">Select BlockChain</InputGroup.Text>
                <select className="form-select" aria-label="Select blockchain" onChange={handleChange}>
                    {blockChains.map(chain => (
                        <option key={chain.id} value={chain.id}>
                            {chain.name}
                        </option>
                    ))}
                </select>
            </InputGroup>
        </Form>
    );
}