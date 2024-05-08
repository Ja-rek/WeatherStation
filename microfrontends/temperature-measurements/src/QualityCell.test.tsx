import React from 'react';
import { render } from '@testing-library/react';
import { QualityCell } from './QualityCell';
import WarningIcon from '@mui/icons-material/Warning';
import ErrorIcon from '@mui/icons-material/Error';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';

jest.mock('@mui/icons-material/Warning', () => jest.fn(() => <div data-testid="warning-icon" />));
jest.mock('@mui/icons-material/Error', () => jest.fn(() => <div data-testid="error-icon" />));
jest.mock('@mui/icons-material/CheckCircle', () => jest.fn(() => <div data-testid="check-circle-icon" />));

describe('QualityCell', () => {
  it('returns WarningIcon for value "Warning"', () => {
    render(<QualityCell value="Warning" />);
    expect(WarningIcon).toHaveBeenCalled();
  });

  it('returns ErrorIcon for value "Alarm"', () => {
    render(<QualityCell value="Alarm" />);
    expect(ErrorIcon).toHaveBeenCalled();
  });

  it('returns CheckCircleIcon for value "Normal"', () => {
    render(<QualityCell value="Normal" />);
    expect(CheckCircleIcon).toHaveBeenCalled();
  });
});