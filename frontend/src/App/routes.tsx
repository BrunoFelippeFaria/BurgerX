import { Route, Routes } from "react-router-dom";
import LoginPage from "../Pages/Auth/LoginPage";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/login" element={<LoginPage/>} />
        </Routes>
    )
}