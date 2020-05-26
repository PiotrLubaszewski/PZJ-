export class AuthenticatedUser {
  public roles: string | string[];
  constructor(public userName: string, public email: string, public firstName: string, public lastName: string) {}
}