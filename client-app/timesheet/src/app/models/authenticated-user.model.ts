export class AuthenticatedUser {
  public roles: string | string[];
  constructor(public userId: string, public userName: string, public email: string, public firstName: string, public lastName: string) {}
}