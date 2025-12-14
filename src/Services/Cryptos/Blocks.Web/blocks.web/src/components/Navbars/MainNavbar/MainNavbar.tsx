import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

import ChainSelect from "../../Dropdowns/ChainSelect/ChainSelect.tsx";

export default function MainNavbar()
{
    return (
        <Navbar expand="lg" className="bg-body-tertiary">
            <Container>
                <Navbar.Brand href="#home">IC Test</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <ChainSelect />
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );

}
