import { Alert, Stack, styled } from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import { useQuery } from "react-query";
import { QualityCell } from "./QualityCell";

const columns: any = [
  { field: "id", headerName: "ID", width: 70 },
  { field: "type", headerName: "Type", flex: 1 },
  { field: "value", headerName: "Value", flex: 1 },
  {
    field: "quality", headerName: "Quality", flex: 1,
    renderCell: (p: any) => <QualityCell value={p.value} />,

  },
];

const ErrorStack = styled(Stack)`
  width: 100%;
`;

export const MeasurementList: React.FC = () => {
  const { isLoading, error, data } = useQuery('data', async () => {
    const response = await fetch('http://localhost:5275/Mesurments');
    return response.json();
  });

  return (
    <>
      {error ?
        (<ErrorStack spacing={2}>
          <Alert severity="error">Conncetion error.</Alert>
        </ErrorStack>)
        : (
          <div style={{ height: 5350, width: "100%" }}>
            <DataGrid
              pageSizeOptions={[]}
              rows={data || []}
              columns={columns}
              loading={isLoading}
              disableRowSelectionOnClick
              disableDensitySelector
            />
          </div>
        )}

    </>
  );
};
