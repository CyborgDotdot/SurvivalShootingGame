using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon[] weaponPrefabs; // ��� ������ ���� ������ �迭
    private List<Weapon> equippedWeapons = new List<Weapon>(); // ���� ������ ���� ���

    public void Start()
    {
        EquipWeapon(0, transform.position);
    }

    // ���⸦ �����ϰ� �����ϴ� �޼���
    public void EquipWeapon(int weaponIndex, Vector3 position)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponPrefabs.Length)
        {
            Debug.LogError("WeaponController: �߸��� ���� �ε����Դϴ�.");
            return;
        }

        // �ش� �ε����� ���� �������� �ν��Ͻ�ȭ�Ͽ� �����մϴ�.
        Weapon newWeapon = Instantiate(weaponPrefabs[weaponIndex], position, Quaternion.identity, transform);
        equippedWeapons.Add(newWeapon);
    }

    // Ư�� ���⸦ �����ϴ� �޼���
    public void UnequipWeapon(Weapon weapon)
    {
        if (equippedWeapons.Contains(weapon))
        {
            equippedWeapons.Remove(weapon);
            Destroy(weapon.gameObject); // ���� ������Ʈ�� �ı��մϴ�.
        }
    }
}
