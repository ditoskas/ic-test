import React from "react";

export default function CenteredBodyLayout({ children }: { children: React.ReactNode }) {
    return (
        <div className="container p-4 h-100">
            {children}
        </div>
    );
}