import { useEffect } from "react";

import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

import CryptoApiRepo from "../../../repositories/CryptoApiRepo.ts";
import type {ApiResponse, Chain} from "../../../types";
import ChainSelect from "../../Dropdowns/ChainSelect/ChainSelect.tsx";
import {useBlocks} from "../../../contexts/BlocksContext/BlocksContext.tsx";

export default function MainNavbar()
{
    const { blockChains, setBlockChains, setLoading } = useBlocks();
    async function fetchChains() {
        setLoading(true);
        const response : ApiResponse<Chain> = await CryptoApiRepo.getChains();
        setBlockChains(response.payload ?? []);
        setLoading(false);
    }

    useEffect(() => {
        fetchChains();
    }, []);
    return (
        <Navbar expand="lg" className="bg-body-tertiary">
            <Container>
                <Navbar.Brand href="#home">IC Test</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <ChainSelect chains={blockChains} />
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );

}
