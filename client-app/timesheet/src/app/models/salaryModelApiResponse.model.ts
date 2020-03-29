import { SalaryModel } from './salaryModel.model'

export class SalaryModelApiResponse {
  result: SalaryModel;
  statusCode: number;
  error:	string;
  validationErrors:	any;
}
