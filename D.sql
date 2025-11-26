-- Решение на 100 баллов.

with recursive target_pickup_point as 
(
	select id, prev_id, bd.amount_rub, id as base_id
	from pickup_point as pp
	left join brand_data as bd
		on bd.pickup_point_id = pp.id
	where branded_since = :targetDate --'2020-03-01'--
		and bd.pickup_point_id is null
	union
	select prev.id, prev.prev_id, bd.amount_rub, base.base_id
	from target_pickup_point as base
	join pickup_point as prev
		on base.prev_id = prev.id
	left join brand_data as bd
		on bd.pickup_point_id = prev.id
)

select distinct base_id 
from target_pickup_point as final_pp
where not exists
(
	select 1 
	from target_pickup_point as check_pp
	where check_pp.base_id = final_pp.base_id
		and check_pp.amount_rub is not null
)