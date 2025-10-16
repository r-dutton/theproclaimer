[web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.MapDataverseIds)  [L581–L627] status=200
  └─ calls UserRepository.ReadQuery [L606]
  └─ query User [L606]
  └─ query BinderStatus [L585]
    └─ reads_from BinderStatuss
  └─ uses_service UserRepository
    └─ method ReadQuery [L606]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery [L9-L41]
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method ReadQuery [L585]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ BinderStatus writes=0 reads=1
      └─ User writes=0 reads=1
    └─ services 2
      └─ IControlledRepository<BinderStatus>
      └─ UserRepository

