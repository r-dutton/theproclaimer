[web] GET /api/companies-house/company/{companyNumber}/registered-office-address  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyRegisteredOfficeAddress)  [L109–L117] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyRegisteredOfficeAddressQuery -> GetCompanyRegisteredOfficeAddressQueryHandler [L114]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyRegisteredOfficeAddressQueryHandler.Handle [L15–L25]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyRegisteredOfficeAddressQuery
    └─ handlers 1
      └─ GetCompanyRegisteredOfficeAddressQueryHandler

