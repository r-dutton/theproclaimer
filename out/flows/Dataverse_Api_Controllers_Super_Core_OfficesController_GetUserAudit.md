[web] GET /api/super/offices/{officeId:Guid}/audit  (Dataverse.Api.Controllers.Super.Core.OfficesController.GetUserAudit)  [L102–L127] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls OfficeRepository.ReadQuery [L105]
  └─ query Office [L105]
    └─ reads_from Offices
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L105]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Office writes=0 reads=1
    └─ services 1
      └─ OfficeRepository

