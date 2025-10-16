[web] GET /api/accounting/reports/labels/accounts/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Layout.ReportLabelController.GetReportLabelsByAccount)  [L59–L72] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to AccountWithReportLabelsDto [L66]
    └─ automapper.registration CirrusMappingProfile (Account->AccountWithReportLabelsDto) [L321]
  └─ calls AccountRepository.ReadQuery [L66]
  └─ query Account [L66]
  └─ uses_service IControlledRepository<Account>
    └─ method ReadQuery [L66]
      └─ ... (no implementation details available)

