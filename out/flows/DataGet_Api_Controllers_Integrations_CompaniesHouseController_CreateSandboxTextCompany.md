[web] POST /api/companies-house/test-data/company  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.CreateSandboxTextCompany)  [L432–L440] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateSandboxTestCompanyCommand -> CreateTestCompanyCommandHandler [L437]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Commands.CreateTestCompanyCommandHandler.Handle [L14–L21]
  └─ impact_summary
    └─ requests 1
      └─ CreateSandboxTestCompanyCommand
    └─ handlers 1
      └─ CreateTestCompanyCommandHandler

