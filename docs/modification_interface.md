# game language

## syntax

<pre>
if (2 + (5 / 2) > 4; "sample"; 'expression') -> "sample"
a := 1 -> a (assign, return variable name)
a := a + 1
b := 3 / a
b -> 1 (in this context)
</pre>

Func_OR_var_NamE = func_or_var_name, but func_or_var_name - origin

### types

| types   | sample                          |
|---------|---------------------------------|
| string  | 'sample string' or "another"    |
| float   | 123 or 1.23 or 1,23 (8 byte)    |
| boolean | (regex pattern) ^(true/yes/on)$ |
| array   | \[-3; 5 / 2; 2 * 2]             |

### operations

| name | sample                 |
|------|------------------------|
| and  | 1 = 1 and true -> true |
| or   | 1 = 1 or off -> true   |
| =    | 5 / 2 = 2 -> true      |
| <    | 1 < 2 -> true          |
| \>   | 3 > 5 -> false         |
| +    | 2 + 5 -> 7             |
| -    | 3 - -5 -> -2           |
| *    | 16 * 16 -> 256         |
| /    | 5 / 2 -> 2.5           |

## global context

### functions

#### internal

| name  | sample                                                     |
|-------|------------------------------------------------------------|
| if    | if (1=1;5;7) -> 5 or if (false;"smth") -> (empty string)   |
| not   | not(1 = 2) -> true or not(on) -> false                     |
| min   | min(4; 2 / 2; 3) -> 1 or min() -> -1.798E+308              |
| max   | max(2 + 1) -> 3 or max() -> 1.798E+308                     |
| log   | log("a="; 10) -> (out to stream a=10)                      |
| error | error("2"; "+"; "3="; 5) -> (like log, but stop thread)    |

#### external

| name                   | input                                         | output |
|------------------------|-----------------------------------------------|--------|
| spawn_interval_trigger | (string) config_name; (array2) spawn_position | void   |

### variables

| name | value    |
|------|----------|
| pi   | 3.141593 |

# config

* parent -> name of inherited config

## spawnable objects

units configs and chance to spawn

## unit

| type     | name          |
|----------|---------------|
| string   | gun_config    |
| string   | bullet_config |
| float    | speed         |

### hooks

#### on_hp_change

call on health change (include unit spawn)

<details>
<summary> local context </summary>
<br>

<b> variables </b>

| type   | name         |
|--------|--------------|
| string | own_name     |
| int    | current_hp   |
| int    | delta_hp     |
| float2 | own_position |

</details>

#### on_die

call when health equals zero

<details>
<summary> local context </summary>
<br>

<b> variables </b>

| type   | name         |
|--------|--------------|
| string | own_name     |
| float2 | own_position |

</details>

## gun

| type  | name          |
|-------|---------------|
| float | scatter       |
| int   | bullets_count |
| float | attack_radius |
| float | attack_speed  |

## bullet

| type   | name       |
|--------|------------|
| float  | damage     |
| script | on_collide |
| float  | force      |
| float4 | color      |

### hooks

#### on_damage

call on bullet collide with unit

<details>
<summary> local context </summary>
<br>

<b> variables </b>

| type   | name       |
|--------|------------|
| string | own_name   |
| string | enemy_name |
| float  | damage     |

<b> function </b>

| type | name         |
|------|--------------|
| int  | get_own_hp   |
| void | set_own_hp   |
| int  | get_enemy_hp |
| void | set_enemy_hp |

</details>

## interval trigger

| type   | name                 |
|--------|----------------------|
| float  | interval             |
| int    | count                |
| bool   | is_enemy_trigger     |
| float2 | collider_size        |
| float2 | collider_offset      |
| float  | collider_edge_radius |
| float  | damage               |
| script | on_trigger           |