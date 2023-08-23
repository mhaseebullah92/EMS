import React from "react";

const layout = (ChildComponent) => {
    const Layout = ({ props }) => {

        return <>
            <div className="container">
                <nav className="navbar navbar-expand-md navbar-dark fixed-top bg-dark">
                    <a className="navbar-brand" href="#">Students</a>

                    <div className="collapse navbar-collapse" id="navbarsExampleDefault">
                        <ul className="navbar-nav mr-auto">
                            <li className="nav-item active">
                                <a className="nav-link" href="#">Students</a>
                            </li>
                        </ul>
                    </div>
                </nav>

                <main role="main">
                    <ChildComponent {...props} />

                </main>
            </div>
        </>
    }

    return Layout;
}

export default layout;