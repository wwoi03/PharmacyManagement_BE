namespace PharmacyManagement_BE.Infrastructure.Customs.Authorization.Requirements
{
    public class RoleRequirementBuilder
    {
        private string _requiredRole = string.Empty;
        private List<string> _requiredUserClaim = new List<string>();
        private List<string> _requiredRoleClaim = new List<string>();

        public RoleRequirementBuilder SetRequiredRole(string role)
        {
            _requiredRole = role;
            return this;
        }

        public RoleRequirementBuilder AddUserClaim(string value)
        {
            _requiredUserClaim.Add(value);
            return this;
        }

        public RoleRequirementBuilder AddRoleClaim(string value)
        {
            _requiredRoleClaim.Add(value);
            return this;
        }

        public RoleRequirement Build()
        {
            return new RoleRequirement(_requiredRole, _requiredRoleClaim, _requiredUserClaim);
        }
    }
}
