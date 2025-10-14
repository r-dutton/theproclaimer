[web] GET /api/companies-house/company/{companyNumber}/registered-office-address  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyRegisteredOfficeAddress)  [L109–L117] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyRegisteredOfficeAddressQuery [L114]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyRegisteredOfficeAddressQueryHandler.Handle [L15–L25]

