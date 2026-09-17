import { Route, Routes } from "react-router-dom";
import LoginPage from "../pages/auth/LoginPage";
import HomePage from "../pages/HomePage";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/login" element={<LoginPage/>} />
            <Route path="/home" element={<HomePage/>} />
        </Routes>
    )
}