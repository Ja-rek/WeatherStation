import { QueryClient, QueryClientProvider } from "react-query";
import { MeasurementList } from "./MeasurementList";
import { CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import { red } from "@mui/material/colors";

const theme = createTheme({
  palette: {
    primary: {
      main: "#556cd6",
    },
    secondary: {
      main: "#19857b",
    },
    error: {
      main: red.A400,
    },
  },
});

const queryClient = new QueryClient();

export default function Root(props: any) {
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <MeasurementList />
      </ThemeProvider>
    </QueryClientProvider>
  );
}
