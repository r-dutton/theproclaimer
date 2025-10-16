[web] DELETE /api/companies-house/test-data/company/{companyNumber}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.DeleteSandboxTextCompany)  [L442–L450] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request DeleteSandboxTestCompanyCommand -> DeleteTestCompanyCommandHandler [L447]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Commands.DeleteTestCompanyCommandHandler.Handle [L16–L26]
  └─ impact_summary
    └─ requests 1
      └─ DeleteSandboxTestCompanyCommand
    └─ handlers 1
      └─ DeleteTestCompanyCommandHandler

