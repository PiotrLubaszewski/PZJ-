export class ApiResponse<T> {
  result: T;
  statusCode: number;
  error: string;
  validationErrors: { [key: string]: string[] };
}
