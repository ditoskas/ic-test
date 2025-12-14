import { Form, InputGroup } from "react-bootstrap";
import React, { useEffect } from "react";
import type { Chain } from "../../../types";

interface ChainSelectInputProps {
    chains: Chain[];
    onChainChange?: (chain: Chain | null) => void;
}

export default function ChainSelect({ chains, onChainChange }: ChainSelectInputProps) {
    useEffect(() => {
        if (chains.length > 0 && onChainChange) {
            onChainChange(chains[0]);
        }
    }, [chains, onChainChange]);

    function handleChange(e: React.ChangeEvent<HTMLSelectElement>) {
        const id = Number(e.target.value);
        const selected = chains.find(c => c.id === id) ?? null;
        if (onChainChange) {
            onChainChange(selected);
        }
    }
    return (
        <Form>
            <InputGroup>
                <InputGroup.Text id="basic-addon1">Select BlockChain</InputGroup.Text>
                <select className="form-select" aria-label="Select blockchain" onChange={handleChange}>
                    {chains.map(chain => (
                        <option key={chain.id} value={chain.id}>
                            {chain.name}
                        </option>
                    ))}
                </select>
            </InputGroup>
        </Form>
    );
}