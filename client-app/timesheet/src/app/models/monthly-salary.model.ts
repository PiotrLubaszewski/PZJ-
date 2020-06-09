export class MonthlySalaryModel {
  date: string;
  days: MonthlySalaryDayModel[];
  salaryAmount: number;
  fullname: string;
  totalFullTimeHours: number;
  totalPredictedHours: number;
  totalWorkedHours: number;
  totalWorkedHoursRatio: number;
  totalDays: number;
  workingDays: number;
}

export class MonthlySalaryDayModel {
  date: string;
  fullTimeHours: number;
  predictedHours: number;
  workedHours: number;
}
