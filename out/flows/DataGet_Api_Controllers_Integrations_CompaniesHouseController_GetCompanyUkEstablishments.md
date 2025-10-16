[web] GET /api/companies-house/company/{companyNumber}/uk-establishments  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyUkEstablishments)  [L254–L262] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyUkEstablishmentsQuery -> GetCompanyUkEstablishmentsQueryHandler [L259]
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyUkEstablishmentsQueryHandler.Handle [L15–L24]
  └─ impact_summary
    └─ requests 1
      └─ GetCompanyUkEstablishmentsQuery
    └─ handlers 1
      └─ GetCompanyUkEstablishmentsQueryHandler

