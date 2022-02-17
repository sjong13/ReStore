import {Container, createTheme, CssBaseline, ThemeProvider, Typography } from "@mui/material";
import { useState } from "react";
import Catalog from "../../features/catalog/Catalog";
import { Product } from "../models/product";
import Header from "./Header";

function App() {
    const [darkMode, setDarkMode] = useState(false);
    const paletteType = darkMode ? 'dark' : 'light';
    const theme = createTheme({
        palette: {
            mode: paletteType,
            background: {
                default: paletteType === 'light' ? '#eaeaea' : '#121212'
            }
        }
    });
    function HandleThemeChange() {
        setDarkMode(!darkMode);
    }
  return (
    <ThemeProvider theme={theme}>
        <CssBaseline/>
        <Header darkMode={darkMode} handleThemeChange={HandleThemeChange}/>
        <Container>
            <Catalog/>
        </Container>
    </ThemeProvider>
  );
}

export default App;
