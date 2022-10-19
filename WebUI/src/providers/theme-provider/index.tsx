import { createTheme, Theme, ThemeProvider as MuiThemeProvider } from '@mui/material/styles';
import { ReactNode } from 'react';
import { CssBaseline } from '@mui/material';

// A custom theme for this app
const theme: Theme = createTheme({
  palette: {
    primary: {
      main: '#77BC1F'
    },
    secondary: {
      main: '#263746'
    }
  }
});

interface ThemeProviderProps {
  children?: ReactNode;
}

export const ThemeProvider = ({ children }: ThemeProviderProps): JSX.Element => {
  return (
    <MuiThemeProvider theme={theme}>
      <CssBaseline />
      {children}
    </MuiThemeProvider>
  );
};
