using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  // UI�C�x���g�V�X�e�����g�p

public class CameraOrbit : MonoBehaviour
{
    public Transform target;  // ���S�ƂȂ�Cube
    public float distance = 10.0f;  // Cube����J�����܂ł̋���
    public float rotationSpeed = 150.0f;  // ��]���x

    private float rotationX = 0.0f;  // X���̉�]
    private float rotationY = 0.0f;  // Y���̉�]
    private Vector2 lastTouchPosition;  // �O��̃^�b�`�ʒu�i�X�}�z�p�j
    private Vector2 lastMousePosition;  // �O��̃}�E�X�ʒu�iPC�p�j
    private bool isTouchingUI = false;  // �ŏ��̃^�b�`��UI�ォ�ǂ������L�^
    private bool isClickingUI = false;  // �ŏ��̃N���b�N��UI�ォ�ǂ������L�^

    void Start()
    {
        // �����̃J�����̊p�x��ݒ�
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;

        // Cube����̋�����ݒ�
        transform.position = target.position - transform.forward * distance;
    }

    void Update()
    {
        // �X�}�z�ł̃^�b�`����
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // �ŏ��̃^�b�`��UI�ォ�ǂ������m�F
                isTouchingUI = IsPointerOverUIObject(touch.position);
                if (!isTouchingUI)
                {
                    lastTouchPosition = touch.position;  // �^�b�`�J�n�ʒu���L�^
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // �ŏ��̃^�b�`��UI��łȂ���Ή�]������
                if (!isTouchingUI)
                {
                    Vector2 delta = touch.position - lastTouchPosition;  // �^�b�`�̈ړ��ʂ��v�Z
                    rotationX += delta.x * rotationSpeed * Time.deltaTime;
                    rotationY -= delta.y * rotationSpeed * Time.deltaTime;

                    // �㉺�̎��_�ړ��𐧌�
                    rotationY = Mathf.Clamp(rotationY, -80f, 80f);

                    lastTouchPosition = touch.position;  // ���݂̃^�b�`�ʒu���L�^
                }
            }
        }
        // PC�ł̃}�E�X����
        else if (Input.GetMouseButtonDown(1))
        {
            // �ŏ��̉E�N���b�N��UI�ォ�ǂ������m�F
            isClickingUI = IsPointerOverUIObject(Input.mousePosition);
            if (!isClickingUI)
            {
                lastMousePosition = Input.mousePosition;  // �}�E�X�̈ʒu���L�^
            }
        }
        else if (Input.GetMouseButton(1))
        {
            // �ŏ��̃N���b�N��UI��łȂ���Ή�]������
            if (!isClickingUI)
            {
                Vector2 delta = (Vector2)Input.mousePosition - lastMousePosition;  // �}�E�X�̈ړ��ʂ��v�Z
                rotationX += delta.x * rotationSpeed * Time.deltaTime;
                rotationY -= delta.y * rotationSpeed * Time.deltaTime;

                // �㉺�̎��_�ړ��𐧌�
                rotationY = Mathf.Clamp(rotationY, -80f, 80f);

                lastMousePosition = Input.mousePosition;  // ���݂̃}�E�X�ʒu���L�^
            }
        }

        // �J�����̈ʒu���X�V
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        transform.position = target.position + rotation * direction;

        // Cube����Ɍ���
        transform.LookAt(target);
    }

    // UI��ł̃^�b�`��N���b�N�𖳎����邽�߂̃w���p�[�֐�
    private bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        // �}�E�X��^�b�`�ʒu�ɂ���UI�I�u�W�F�N�g���m�F
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        // ���C�L���X�g���s���āAUI�I�u�W�F�N�g�����邩���m�F
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
