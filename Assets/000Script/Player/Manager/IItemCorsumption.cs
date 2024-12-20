using System.Collections.Generic;
public interface IItemConsumption
{
    void inItem(string itemId, int num);
    void subItemQuantity(string item_id, int num);

    //�o�b�O�ɓ���邱�Ƃ��ł���A�C�e����id�̃��X�g
    List<string> createInItemArgumentList();

    //�o�b�O�̒��g
    Dictionary<string, int> getBagSummary();

    //�o�b�O�������ς����ǂ���
    bool isMaxBag();
}