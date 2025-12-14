import MainNavbar from "../../components/Navbars/MainNavbar/MainNavbar.tsx";
import CenteredBodyLayout from "../BodyLayout/CenteredBodyLayout.tsx";
import FooterLayout from "../FooterLayout/FooterLayout.tsx";

export default function MainLayout({ children, numberOfRecords }: { children: React.ReactNode, numberOfRecords: number }) {
    return (
        <div className="container-fluid h-100 d-flex flex-column p-0">
            <MainNavbar />
            <CenteredBodyLayout>
                {children}
            </CenteredBodyLayout>
            <FooterLayout numberOfRecords={numberOfRecords} />
        </div>
    );
}