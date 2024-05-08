import WarningIcon from '@mui/icons-material/Warning';
import ErrorIcon from '@mui/icons-material/Error';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import { styled } from '@mui/material';

export interface QualityCellProps {
    value: string
}

const StyledSpan = styled('span')`
  margin-left: 8ps;
`;

export const QualityCell = ({ value }: QualityCellProps) => {
    let icon;
    switch (value) {
        case "Warning":
            icon = <WarningIcon color="warning" />;
            break;
        case "Alarm":
            icon = <ErrorIcon color="error" />;
            break;
        case "Normal":
            icon = <CheckCircleIcon color="success" />;
            break;
    }

    return (
        <>
            {icon}
            <StyledSpan>{value}</StyledSpan>
        </>
    );
};
