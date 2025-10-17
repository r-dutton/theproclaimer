[web] GET /api/ui/offices/{id}/find-office-users  (Dataverse.Api.Controllers.UI.Core.OfficesController.FindOfficeUsers)  [L109–L130] status=200 [auth=Authentication.UserPolicy]
  └─ calls OfficeRepository.ReadQuery [L116]
  └─ query Office [L116]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L116]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository

