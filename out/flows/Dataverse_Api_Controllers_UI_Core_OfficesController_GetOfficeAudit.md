[web] GET /api/ui/offices/{id:Guid}/audit  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetOfficeAudit)  [L181–L206] status=200 [auth=Authentication.UserPolicy]
  └─ calls OfficeRepository.ReadQuery [L184]
  └─ query Office [L184]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L184]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository

