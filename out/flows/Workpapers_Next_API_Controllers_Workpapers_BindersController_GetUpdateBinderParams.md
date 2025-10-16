[web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.GetUpdateBinderParams)  [L518–L539] status=200
  └─ calls BinderColumnDataRepository.WriteQuery [L531]
  └─ write BinderColumnData [L531]
    └─ reads_from BinderColumnData
  └─ query BinderStatus [L523]
    └─ reads_from BinderStatuss
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method ReadQuery [L523]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 2 (writes=1, reads=1)
      └─ BinderColumnData writes=1 reads=0
      └─ BinderStatus writes=0 reads=1
    └─ services 1
      └─ IControlledRepository<BinderStatus>

