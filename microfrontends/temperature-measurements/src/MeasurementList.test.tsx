import React from 'react';
import { render } from '@testing-library/react';
import { MeasurementList } from './MeasurementList';
import { useQuery } from 'react-query';

jest.mock('react-query');

const mockedUseQuery = useQuery as jest.MockedFunction<typeof useQuery>;

describe('MeasurementList', () => {
  afterEach(() => {
    jest.clearAllMocks();
  });
  it('renders Stack and Alert components with error message when there is a connection error2', () => {
    const errorMessage = 'Conncetion error.';
    mockedUseQuery.mockReturnValueOnce({ isLoading: false, error: true, data: undefined } as any);

    const { getByText } = render(<MeasurementList />);


    const alert = getByText(errorMessage);
    expect(alert).toBeInTheDocument();
  });
});
