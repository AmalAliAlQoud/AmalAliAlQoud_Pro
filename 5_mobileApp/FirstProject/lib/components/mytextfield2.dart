import 'package:flutter/material.dart';

class MyTextField extends StatelessWidget {
  const MyTextField({
    super.key,
    required this.label,
    required this.hint,
    this.obscureText = false,
  });

  final String label;
  final String hint;
  final bool obscureText;

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          label,
          style: const TextStyle(
            fontSize: 20,
            color: Colors.purple,
            fontWeight: FontWeight.w700,
          ),
        ),
        const SizedBox(height: 8),
        TextFormField(
          obscureText: obscureText,
          decoration: InputDecoration(
            hintText: hint,
            enabledBorder: const OutlineInputBorder(
              borderSide: BorderSide(color: Colors.green, width: 1),
              borderRadius: BorderRadius.all(Radius.circular(20)),
            ),
            focusedBorder: const OutlineInputBorder(
              borderSide: BorderSide(color: Colors.green, width: 1),
              borderRadius: BorderRadius.all(Radius.circular(20)),
            ),
          ),
        ),
      ],
    );
  }
}